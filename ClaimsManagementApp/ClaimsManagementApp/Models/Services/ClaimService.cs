using ClaimsManagementApp.Models;
using System.Text.Json;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System;

namespace ClaimsManagementApp.Services
{
    public class ClaimService : IClaimService
    {
        private List<Claim> _claims;
        private readonly string _dataFile = "claims.json";
        private readonly IFileService _fileService;

        public ClaimService(IFileService fileService)
        {
            _fileService = fileService;
            _claims = new List<Claim>(); // Initialize to avoid null reference
            LoadClaims();
        }

        private void LoadClaims()
        {
            try
            {
                if (File.Exists(_dataFile))
                {
                    var json = File.ReadAllText(_dataFile);
                    var loadedClaims = JsonSerializer.Deserialize<List<Claim>>(json);
                    _claims = loadedClaims ?? new List<Claim>();
                }
            }
            catch (Exception ex)
            {
                // If loading fails, start with empty list
                _claims = new List<Claim>();
                Console.WriteLine($"Error loading claims: {ex.Message}");
            }
        }

        private void SaveClaims()
        {
            try
            {
                var json = JsonSerializer.Serialize(_claims, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_dataFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving claims: {ex.Message}");
            }
        }

        public List<Claim> GetAllClaims() => _claims;

        public Claim? GetClaimById(int id) => _claims.FirstOrDefault(c => c.Id == id);

        public void AddClaim(Claim claim)
        {
            claim.Id = _claims.Count > 0 ? _claims.Max(c => c.Id) + 1 : 1;
            _claims.Add(claim);
            SaveClaims();
        }

        public void UpdateClaimStatus(int claimId, ClaimStatus status)
        {
            var claim = GetClaimById(claimId);
            if (claim != null)
            {
                claim.Status = status;
                SaveClaims();
            }
        }

        public List<Claim> GetClaimsByStatus(ClaimStatus status)
        {
            return _claims.Where(c => c.Status == status).ToList();
        }

        public void AddDocumentToClaim(int claimId, SupportingDocument document)
        {
            var claim = GetClaimById(claimId);
            if (claim != null)
            {
                document.Id = claim.Documents.Count > 0 ? claim.Documents.Max(d => d.Id) + 1 : 1;
                document.ClaimId = claimId;
                claim.Documents.Add(document);
                SaveClaims();
            }
        }

        public SupportingDocument? GetDocument(int documentId)
        {
            return _claims.SelectMany(c => c.Documents)
                         .FirstOrDefault(d => d.Id == documentId);
        }

        // Settlement methods
        public void SettleClaim(int claimId, string settlementNotes, string paymentReference, string processedBy)
        {
            var claim = GetClaimById(claimId);
            if (claim != null && claim.Status == ClaimStatus.ApprovedByManager)
            {
                claim.Status = ClaimStatus.Settled;
                claim.SettlementDate = DateTime.Now;
                claim.SettlementNotes = settlementNotes;
                claim.PaymentReference = paymentReference;
                claim.ProcessedBy = processedBy;
                SaveClaims();
            }
        }

        public List<Claim> GetClaimsReadyForSettlement()
        {
            return _claims.Where(c => c.Status == ClaimStatus.ApprovedByManager).ToList();
        }

        public List<Claim> GetSettledClaims()
        {
            return _claims.Where(c => c.Status == ClaimStatus.Settled).ToList();
        }
    }
}