namespace LampStore.Models
{
	public interface IInfoRepository
	{
		IQueryable<Info> Info { get; }
		IQueryable<InfoProp> InfoProp { get; }
		void SaveInfo(Info i);
		void CreateInfo(Info i);
		void DeleteInfo(Info i);

		void CreateAdditionalField(InfoProp i);
		void SaveAdditionalField(InfoProp i);
		void DeleteAdditionalField(InfoProp i);
	}
}