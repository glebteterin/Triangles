using AutoMapper;

namespace Triangles.Web
{
	internal static class LocalBootstrapper
	{
		public static void InitializeAutoMapper()
		{
			Mapper.CreateMap<Web.Models.Receipt, Code.DataAccess.Receipt>()
				.ForMember(dst => dst.ReceiptRecords, opt => opt.Ignore())
				.ForMember(dst => dst.Session, opt => opt.Ignore())
			;

			Mapper.CreateMap<Code.DataAccess.Receipt,Web.Models.Receipt>();
		}
	}
}