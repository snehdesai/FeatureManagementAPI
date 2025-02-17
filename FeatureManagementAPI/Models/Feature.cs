﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FeatureManagementAPI.Models
{
    public class Feature
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Title { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public string EstimatedComplexity { get; set; } // S, M, L, XL

        [Required]
        public string Status { get; set; } // New, Active, Closed, Abandoned

        [FutureDate(ErrorMessage = "Target Completion Date must be a future date.")]
        public DateTime? TargetCompletionDate { get; set; }

        [FutureDate(ErrorMessage = "Actual Completion Date must be a future date.")]
        public DateTime? ActualCompletionDate { get; set; }
    }
}