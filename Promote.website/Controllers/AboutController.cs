using Microsoft.AspNetCore.Mvc;
using Promote.website.Models;

namespace Promote.website.Controllers
{
    public class AboutController : Controller
    {
        private readonly Context _context;

        public AboutController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var values = _context.aboutPages.FirstOrDefault(); // AboutPage modelinin DbSet'i üzerinden verileri çekiyoruz
            if (values != null)
            {
                return View(values);
            }
            else
            { 
                var defaultValue = new AboutPage
                {
                    CompanyDescription = "Touristt, tutkuyla seyahat etmeyi seven bir grup profesyonel gezgin tarafından kurulmuş bir tur şirketidir. Touristt, müşterilerine unutulmaz deneyimler sunma hedefiyle yola çıkmıştır. Sizlere dünya genelinde benzersiz destinasyonlarda eşsiz anlar yaşatmayı amaçlıyoruz.",
                    MissionDescription = "Misyonumuz, müşterilerimize sadece bir tatil değil, aynı zamanda bir yaşam deneyimi sunmaktır. Her detayı özenle planlanmış turlarımız, kültürel zenginlikleri keşfetmenize ve unutulmaz anılar biriktirmenize olanak tanır. Seyahatinizi özel ve anlamlı kılmak için buradayız.\r\n\r\n",
                    MissionTitle = "Misyonumuz: Seyahatlerinizi Özel Kılmak",
                    VisionDescription = "Vizyonumuz, sınırları aşarak dünyayı keşfetme arzusuyla yanıp tutuşan herkesi bir araya getirmektir. [Şirket Adı] olarak, gezgin ruhumuzla dünya genelinde benzersiz ve etkileyici destinasyonları sizinle buluşturuyoruz. Vizyonumuz, seyahatinizi bir maceraya dönüştürmeniz için sizi cesaretlendirmek ve ilham vermek.",
                    VisionTitle = "Vizyonumuz: Sınırları Aşan Keşifler",
                    WhyUs1Description= "Touristt ekibi, sektörde deneyimli ve seyahat tutkunu profesyonellerden oluşur.",
                    WhyUs1Title="Profesyonel ve Deneyimli Ekip",
                    WhyUs2Description = "Özenle seçilmiş destinasyonlarla unutulmaz bir seyahat deneyimi yaşayın.",
                    WhyUs2Title = "Benzersiz Destinasyonlar",
                    WhyUs3Description = "Müşterilerimizin memnuniyeti, en yüksek önceliğimizdir.",
                    WhyUs3Title = "Müşteri Memnuniyeti Odaklı",
                    WhyUsSectionTitle="Neden Biz?",
                    VisionBgColor= "#fbc02d",
                    MissionBgColor= "#fdd835",
                    WhyUsSectionBgColor="#fff",
                    WhyUs1BgColor= "#262a2b",
                    WhyUs2BgColor= "#262a2b",
                    WhyUs3BgColor= "#262a2b",
                    
                };
                return View(defaultValue);
            }
        }
    }
}

