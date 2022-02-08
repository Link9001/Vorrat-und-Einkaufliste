namespace HouseholdmanagementTool.Attributes.UserObjectCreation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class IgnoreForUserCreationAttribute : Attribute
{
    public IgnoreForUserCreationAttribute(bool skip)
    {
        Skip = skip;
    }

    public bool Skip { get; set; }

}