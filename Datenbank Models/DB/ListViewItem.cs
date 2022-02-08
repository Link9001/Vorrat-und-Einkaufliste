using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows.Media;
using HouseholdmanagementTool.UtitlityFunctions;

namespace Database_Models.DB;
internal abstract record ListViewItem : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    [JsonIgnore]
    private Brush _status = new SolidColorBrush(Colors.Transparent);

    [JsonIgnore]
    public Brush Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged(nameof(_status));
        }
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}