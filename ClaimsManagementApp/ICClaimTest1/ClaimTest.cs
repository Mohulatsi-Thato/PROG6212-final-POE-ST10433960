using Xunit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ICClaimTest1
{
    // Temporary simplified Claim class for testing
    public class TemporaryClaim
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lecturer name is required")]
        public string LecturerName { get; set; }

        [Required(ErrorMessage = "Hours worked is required")]
        [Range(0.5, 100, ErrorMessage = "Hours worked must be between 0.5 and 100")]
        public decimal HoursWorked { get; set; }

        [Required(ErrorMessage = "Hourly rate is required")]
        [Range(50, 500, ErrorMessage = "Hourly rate must be between 50 and 500")]
        public decimal HourlyRate { get; set; }

        public decimal TotalAmount => HoursWorked * HourlyRate;

        public string AdditionalNotes { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";
    }

    public class ClaimTest
    {
        // ✅ Test 1: Verify TotalAmount is calculated correctly
        [Fact]
        public void CalculatedTotalAmount()
        {
            var claim = new TemporaryClaim
            {
                HoursWorked = 23,
                HourlyRate = 690
            };

            var getResult = claim.TotalAmount;

            Assert.Equal(15870, getResult);
        }

        // ✅ Test 2: Verify AdditionalNotes assignment
        [Fact]
        public void AdditionalNotesSimulation()
        {
            var claim = new TemporaryClaim
            {
                AdditionalNotes = "Test for Claim notes"
            };

            Assert.Equal("Test for Claim notes", claim.AdditionalNotes);
        }

        // ✅ Test 3: Valid Lecturer Name should pass validation
        [Fact]
        public void ValidLecturerName_PassesValidation()
        {
            var claim = new TemporaryClaim
            {
                LecturerName = "Dr. Kgosi Mohulatsi",
                HoursWorked = 10,
                HourlyRate = 150
            };

            var validationResults = ValidateModel(claim);
            Assert.Empty(validationResults); // no validation errors
        }

        // ✅ Test 4: Missing Lecturer Name should fail validation
        [Fact]
        public void MissingLecturerName_FailsValidation()
        {
            var claim = new TemporaryClaim
            {
                HoursWorked = 10,
                HourlyRate = 150
            };

            var validationResults = ValidateModel(claim);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Lecturer name is required"));
        }

        // ✅ Test 5: HoursWorked below range should fail
        [Fact]
        public void HoursWorkedBelowMinimum_FailsValidation()
        {
            var claim = new TemporaryClaim
            {
                LecturerName = "Mr. Test",
                HoursWorked = 0.3m,
                HourlyRate = 100
            };

            var validationResults = ValidateModel(claim);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Hours worked must be between 0.5 and 100"));
        }

        // ✅ Helper method for validation
        private List<ValidationResult> ValidateModel(object model)
        {
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
    }
}