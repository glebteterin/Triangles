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

			Mapper.CreateMap<Code.DataAccess.ReceiptRecord,Web.Models.ReceiptRecord>();
			Mapper.CreateMap<Web.Models.ReceiptRecord, Code.DataAccess.ReceiptRecord>();
		}
	}
}