using EPi7Test.Examples.Models.Pages;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using Machine.Specifications;

namespace EPi7Test.Examples.Tests.PriceExample
{
    [Subject("PriceExample")]
    public class When_original_price_is_4000_with_no_discount : TestContext
    {
        private static HotelPage _hotelPage;
        private static ContentReference _hotelPageRef;

        Establish context = () =>
            {
                _hotelPage = ContentRepository.GetDefault<HotelPage>(ContentReference.RootPage);
                _hotelPage.PageName = "Hotel California";
                _hotelPage.PriceOriginal = 4000;
                _hotelPage.PriceDiscount = 0;

                _hotelPageRef = ContentRepository.Save(_hotelPage, SaveAction.Publish, AccessLevel.NoAccess);
            };

        Because of = () => _hotelPage = ContentRepository.Get<HotelPage>(_hotelPageRef);

        It should_should_display_2000_as_price_per_person = () => _hotelPage.PriceToPayPerPerson.ShouldEqual(2000);

        It should_not_display_price_as_discounted = () => _hotelPage.PriceIsDiscounted.ShouldBeFalse();
    }
}
