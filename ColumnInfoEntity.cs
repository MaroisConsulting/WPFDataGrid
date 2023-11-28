using System.ComponentModel;

namespace WPFDataGrid
{
    public class ColumnInfoEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }

        private string _ColumnName;
        public string ColumnName
        {
            get { return _ColumnName; }
            set
            {
                if (_ColumnName != value)
                {
                    _ColumnName = value;
                    RaisePropertyChanged(nameof(ColumnName));
                }
            }
        }

        private double _ColumnWidth;
        public double ColumnWidth
        {
            get { return _ColumnWidth; }
            set
            {
                if (_ColumnWidth != value)
                {
                    _ColumnWidth = value;
                    RaisePropertyChanged(nameof(ColumnWidth));
                }
            }
        }

        public ColumnInfoEntity()
        {
            
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
