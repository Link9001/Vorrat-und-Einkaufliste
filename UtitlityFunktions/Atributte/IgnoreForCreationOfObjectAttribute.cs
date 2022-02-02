namespace UtitlityFunctions.Atributte;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class IgnoreForCreationOfObjectAttribute : Attribute
{
    public IgnoreForCreationOfObjectAttribute(bool skip)
    {
        Skip = skip;
    }

    public bool Skip { get; set; }

}