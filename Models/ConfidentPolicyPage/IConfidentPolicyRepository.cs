namespace LampStore.Models
{
	public interface IConfidentPolicyRepository
	{
		IQueryable<ConfidentPolicy> ConfidentPolicys { get; }

		void SavePolicy(ConfidentPolicy c);
		void CreatePolicy(ConfidentPolicy c);
		void DeletePolicy(ConfidentPolicy c);

	}
}