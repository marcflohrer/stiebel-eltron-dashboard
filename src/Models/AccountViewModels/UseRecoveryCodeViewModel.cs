﻿using System.ComponentModel.DataAnnotations;

namespace StiebelEltronDashboard.Models.AccountViewModels
{
    public class UseRecoveryCodeViewModel
    {
        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }
    }
}
