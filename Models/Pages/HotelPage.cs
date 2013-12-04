using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.DataAnnotations;

namespace EPi7Test.Examples.Models.Pages
{
    [ContentType(
        DisplayName = "Hotel page",
        GUID = "351F587F-1DDB-445A-9DDA-CD2BE37B89F8")]
    public class HotelPage : PageData
    {
        public int PriceOriginal { get; set; }
        public int PriceDiscount { get; set; }
        public int PriceToPayPerPerson { get; set; }
        public bool PriceIsDiscounted { get; set; }
        public bool HasTips { get; set; }
        public List<TravelTipsPage> TipsList { get; set; }
    }
}
