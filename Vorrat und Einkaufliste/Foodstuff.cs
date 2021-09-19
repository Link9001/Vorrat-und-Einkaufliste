using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Vorrat_und_Einkaufliste
{
    internal class Foodstuff : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime dateTime;
        public DateTime DateTime {
            get => dateTime;
            set
            {
                Date = $"{value.Day}.{value.Month}.{value.Year}";
                dateTime = value;
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _date;
        [JsonIgnore]
        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private string _placement;
        public string Placement
        {
            get => _placement;
            set
            {
                _placement = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Foodstuff> _relatedFoodlist;

        public ObservableCollection<Foodstuff> RelatedFoodList
        {
            get => _relatedFoodlist;
            set
            {
                _relatedFoodlist = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        [JsonConstructor]
        public Foodstuff(string Name, DateTime DateTime, string Placement, ObservableCollection<Foodstuff> RelatedFoodList)
        {
            this.DateTime = DateTime;
            this.Name = Name;
            this.Placement = Placement;
            this.RelatedFoodList = RelatedFoodList;
        }
        public Foodstuff(string Name, DateTime DateTime, Placement Placement, ObservableCollection<Foodstuff> RelatedFoodList)
        {
            this.DateTime = DateTime;
            this.Name = Name;
            this.Placement = Placement.ToString();
            this.RelatedFoodList = RelatedFoodList;
        }
    }
}
