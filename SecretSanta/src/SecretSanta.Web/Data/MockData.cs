using System.Collections.Generic;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Data
{
    public static class MockData
    {
        public static List<UserViewModel> Users = new List<UserViewModel>
        {
            new UserViewModel{FirstName = "John ", LastName="Smith"},
            new UserViewModel{FirstName = "Jane ", LastName="Smith"}
        };

        public static List<GroupViewModel> Groups = new List<GroupViewModel>
        {
            new GroupViewModel{GroupName="IntelliTect"},
            new GroupViewModel{GroupName="Microsoft"}
        };

        public static List<GiftViewModel> Gifts = new List<GiftViewModel>
        {
            new GiftViewModel{Title="Star Wars Lego", Description="Imperial Star Destroyer", Url="website.com", Priority=1, UserID=0},
            new GiftViewModel{Title="Fallout 4", Description="Xbox One GOTY Edtion", Url="website.com", Priority=2, UserID=1}
        };
    }
}