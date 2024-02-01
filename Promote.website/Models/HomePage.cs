using System.ComponentModel.DataAnnotations;

namespace Promote.website.Models
{
    public class HomePage
    {
        [Key]
        public int HomeId { get; set; }

        [Display(Name = "Video File Name")]
        public string? VideoFileName { get; set; }

        [Display(Name = "Is Tagline Section Included")]
        public bool  IsTaglineSectionIncluded { get; set; }

        [Display(Name = "Tagline Section Background Color")]
        public string? TaglineSectionBgColor { get; set; }

        [Display(Name = "Tagline")]
        public string? Tagline { get; set; }

        [Display(Name = "Is Popular Products Section Included")]
        public bool  IsPopularProductsSectionIncluded { get; set; }

        [Display(Name = "Popular Products Section Title")]
        public string? PopularProductsSectionTitle { get; set; }

        [Display(Name = "Popular Products Section Background Color")]
        public string? PopularProductsSectionBgColor { get; set; }

        [Display(Name = "Popular Product 1 Title")]
        public string? PopularProduct1Title { get; set; }

        [Display(Name = "Popular Product 1 Image")]
        public string? PopularProduct1Image { get; set; }

        [Display(Name = "Popular Product 1 Id")]
        public int? PopularProduct1Id { get; set; }

        [Display(Name = "Popular Product 2 Title")]
        public string? PopularProduct2Title { get; set; }

        [Display(Name = "Popular Product 2 Image")]
        public string? PopularProduct2Image { get; set; }

        [Display(Name = "Popular Product 2 Id")]
        public int? PopularProduct2Id { get; set; }

        [Display(Name = "Popular Product 3 Title")]
        public string? PopularProduct3Title { get; set; }

        [Display(Name = "Popular Product 3 Image")]
        public string? PopularProduct3Image { get; set; }

        [Display(Name = "Popular Product 3 Id")]
        public int? PopularProduct3Id { get; set; }

        [Display(Name = "Popular Product 4 Title")]
        public string? PopularProduct4Title { get; set; }

        [Display(Name = "Popular Product 4 Image")]
        public string? PopularProduct4Image { get; set; }

        [Display(Name = "Popular Product 4 Id")]
        public int? PopularProduct4Id { get; set; }

        [Display(Name = "Is Services Section Included")]
        public bool  IsServicesSectionIncluded { get; set; }

        [Display(Name = "Services Section Title")]
        public string? ServicesSectionTitle { get; set; }

        [Display(Name = "Services Section Background Color")]
        public string? ServicesSectionBgColor { get; set; }

        [Display(Name = "Service 1 Image")]
        public string? Services1Image { get; set; }

        [Display(Name = "Service 1 Description")]
        public string? Services1Description { get; set; }

        [Display(Name = "Service 2 Image")]
        public string? Services2Image { get; set; }

        [Display(Name = "Service 2 Description")]
        public string? Services2Description { get; set; }

        [Display(Name = "Service 3 Image")]
        public string? Services3Image { get; set; }

        [Display(Name = "Service 3 Description")]
        public string? Services3Description { get; set; }

        [Display(Name = "Service 4 Image")]
        public string? Services4Image { get; set; }

        [Display(Name = "Service 4 Description")]
        public string? Services4Description { get; set; }

        [Display(Name = "Is Statistics Section Included")]
        public bool  IsStatisticsSectionIncluded { get; set; }

        [Display(Name = "Statistic Section Background Color")]
        public string? StatisticSectionBgColor { get; set; }

        [Display(Name = "Statistic 1 Number")]
        public int? Statistic1Number { get; set; }

        [Display(Name = "Statistic 1 Title")]
        public string? Statistic1Title { get; set; }

        [Display(Name = "Statistic 2 Number")]
        public int? Statistic2Number { get; set; }

        [Display(Name = "Statistic 2 Title")]
        public string? Statistic2Title { get; set; }

        [Display(Name = "Statistic 3 Number")]
        public int? Statistic3Number { get; set; }

        [Display(Name = "Statistic 3 Title")]
        public string? Statistic3Title { get; set; }

        [Display(Name = "Statistic 4 Number")]
        public int? Statistic4Number { get; set; }

        [Display(Name = "Statistic 4 Title")]
        public string? Statistic4Title { get; set; }
    }
}
