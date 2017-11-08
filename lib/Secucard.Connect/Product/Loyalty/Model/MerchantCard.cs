namespace Secucard.Connect.Product.Loyalty.Model
{
    using System;
    using System.Runtime.Serialization;
    using Secucard.Connect.Net.Util;
    using Secucard.Connect.Product.Common.Model;
    using Secucard.Connect.Product.General.Model;

    [DataContract]
    public class MerchantCard : SecuObject
    {
        // Deprecated use "PasscodeProtectionOptionGeneral"
        public const int PASSCODE_PROTECTION_OPTION_GENERAL = 1;

        // Deprecated use "PasscodeProtectionOptionDischarge"
        public const int PASSCODE_PROTECTION_OPTION_DISCHARGE = 2;

        // Deprecated use "PasscodeProtectionOptionCharge"
        public const int PASSCODE_PROTECTION_OPTION_CHARGE = 3;

        // Deprecated use "PasscodeProtectionOptionRevenue"
        public const int PASSCODE_PROTECTION_OPTION_REVENUE = 4;

        // Deprecated use "PasscodeProtectionOptionChargePoints"
        public const int PASSCODE_PROTECTION_OPTION_CHARGE_POINTS = 5;

        // Deprecated use "PasscodeProtectionOptionDischargePoints"
        public const int PASSCODE_PROTECTION_OPTION_DISCHARGE_POINTS = 6;

        public const int PasscodeProtectionOptionGeneral = 1;
        public const int PasscodeProtectionOptionDischarge = 2;
        public const int PasscodeProtectionOptionCharge = 3;
        public const int PasscodeProtectionOptionRevenue = 4;
        public const int PasscodeProtectionOptionChargePoints = 5;
        public const int PasscodeProtectionOptionDischargePoints = 6;

        public const int PasscodeStatusNotEnabled = 1;
        public const int PasscodeStatusNotSet = 2;
        public const int PasscodeStatusSet = 3;

        private DateTime? lastCharge;
        private DateTime? lastUsage;

        [DataMember(Name = "balance")]
        public int Balance { get; set; }

        [DataMember(Name = "card")]
        public Card Card { get; set; }

        [DataMember(Name = "cardgroup")]
        public CardGroup Cardgroup { get; set; }

        [DataMember(Name = "created_for_merchant")]
        public Merchant CreatedForMerchant { get; set; }

        [DataMember(Name = "created_for_store")]
        public Store CreatedForStore { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "is_base_card")]
        public bool IsBaseCard { get; set; }

        [DataMember(Name = "lock_status")]
        public string LockStatus { get; set; }

        [DataMember(Name = "merchant")]
        public Merchant Merchant { get; set; }

        [DataMember(Name = "points")]
        public int Points { get; set; }

        [DataMember(Name = "stock_status")]
        public string StockStatus { get; set; }

        [DataMember(Name = "last_usage")]
        public string FormattedLastUsage
        {
            get { return this.lastUsage.ToDateTimeZone(); }
            set { this.lastUsage = value.ToDateTime(); }
        }

        [DataMember(Name = "last_charge")]
        public string FormattedLastCharge
        {
            get { return this.lastCharge.ToDateTimeZone(); }
            set { this.lastCharge = value.ToDateTime(); }
        }

        [DataMember(Name = "cash_balance")]
        public int CashBalance { get; set; }

        [DataMember(Name = "bonus_balance")]
        public int BonusBalance { get; set; }

        [DataMember(Name = "passcode")]
        public int Passcode { get; set; }
    }
}