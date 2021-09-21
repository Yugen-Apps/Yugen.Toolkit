namespace Yugen.Toolkit.Uwp.CodeChallenge.Model
{
    public class ValueModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Claim { get; set; }
        public bool HasClaim => string.IsNullOrEmpty(Claim) == false;
        public int Order { get; set; }
    }
}
