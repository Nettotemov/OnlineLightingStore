namespace LampStore.Models
{
	public class AboutPage
	{
		public long? ID { get; set; }

		public string? ImgOneUrl { get; set; } = string.Empty;

		public string? VideoOneUrl { get; set; } = string.Empty;
		
		public string HeadingOneNode { get; set; } = string.Empty;
		
		public string ParagraphOneNode { get; set; } = string.Empty;

		public string? ImgTwoUrl { get; set; } = string.Empty;

		public string? VideoTwoUrl { get; set; } = string.Empty;

		public string HeadingTwoNode { get; set; } = string.Empty;
		
		public string ParagraphTwoNode { get; set; } = string.Empty;

		public bool DisplayHomePage { get; set; }


	}
}