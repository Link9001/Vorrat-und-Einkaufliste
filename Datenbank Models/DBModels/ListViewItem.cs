using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows.Media;
using HouseholdmanagementTool.UtitlityFunctions;

namespace Database_Models.DBModels;
internal abstract record ListViewItem : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    [JsonIgnore]
    private Brush status = new SolidColorBrush(Colors.Transparent);

    [JsonIgnore]
    public Brush Status
    {
        get => status;
        set
        {
            status = value;
            OnPropertyChanged(nameof(status));
        }
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}