using System.Linq;
using AutoMapper;
using Triangles.Code.BusinessLogic.Receipt;

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

			Mapper.CreateMap<Code.DataAccess.ReceiptRecord, Code.BusinessLogic.Receipt.ReceiptRecord>();

			Mapper.CreateMap<Code.DataAccess.Receipt, Code.BusinessLogic.Receipt.Receipt>()
				.AfterMap((src, dst) => dst.Records = src.ReceiptRecords.Select(Mapper.Map<ReceiptRecord>).ToArray());
		}
	}
}