using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountRegisterApplication.Models.WbBuyer
{
    internal class PersonalInfoModel
    {
        public int ResultState { get; set; }
        public Value Value { get; set; }
    }
    public class MaskedInfo
    {
        public string ThirdName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string FormattedPhoneMobile { get; set; }
        public string Email { get; set; }
        public int Inn { get; set; }
        public string UserPhotoLink { get; set; }
        public string FullName { get; set; }
        public string GenderE { get; set; }
        public bool IsIdentityVerified { get; set; }
    }

    public class Value
    {
        public bool IsPickupPointOwner { get; set; }
        public MaskedInfo MaskedInfo { get; set; }
        public bool IsActiveEmployee { get; set; }
        public double PurchaseAmount { get; set; }
        public int PersonalDiscount { get; set; }
        public bool IsIdentityVerified { get; set; }
        public string Country { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsImpersonated { get; set; }
        public bool IsUnknownUser { get; set; }
        public string ThirdName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string UserPhotoLink { get; set; }
        public string Email { get; set; }
        public bool HasPhoto { get; set; }
        public long Phone { get; set; }
        public int Inn { get; set; }
        public string FormattedPhoneMobile { get; set; }
        public bool IsEmployeeSynced { get; set; }
        public bool EmailDoesNotExist { get; set; }
        public bool PhoneMobileConfirmed { get; set; }
        public string BirthdayDate { get; set; }
        public DateTime BirthdayRaw { get; set; }
        public string GenderE { get; set; }
        public string LocaleShortName { get; set; }
        public string SomeId { get; set; }
        public int Id { get; set; }
        public string Initials { get; set; }
    }
}
