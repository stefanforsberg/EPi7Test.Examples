using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace EPi7Test.Examples.Models.Pages
{
    [ContentType(
        DisplayName = "Travel tips page",
        GUID = "3BF63373-BFFD-43D1-985A-5C362DD55CDA")]
    public class TravelTipsPage : PageData
    {
        public ContentReference RelatedHotelPage { get; set; }
    }
}
