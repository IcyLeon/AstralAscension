public class AscensionInformation
{
    public AscensionInformation nextNode { get; private set; }
    public AscensionInformation parentNode { get; private set; }
    public Ascension ascension { get; private set; }

    public AscensionInformation(Ascension Ascension, AscensionInformation Parent)
    {
        ascension = Ascension;
        parentNode = Parent;
    }

    public void CreateNode(AscensionInfoStat AscensionInfoStat)
    {
        if (nextNode != null)
        {
            nextNode.CreateNode(AscensionInfoStat);
            return;
        }

        nextNode = new AscensionInformation(AscensionInfoStat.CreateAscension(), parentNode);
    }
}
