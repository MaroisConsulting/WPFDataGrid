using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WPFDataGrid
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ColumnInfoEntity> _ColumnInfos;
        public ObservableCollection<ColumnInfoEntity> ColumnInfos
        {
            get { return _ColumnInfos; }
            set
            {
                if (_ColumnInfos != value)
                {
                    _ColumnInfos = value;
                    RaisePropertyChanged(nameof(ColumnInfos));
                }
            }
        }

        private ColumnInfoEntity _SelectedColumnInfo;
        public ColumnInfoEntity SelectedColumnInfo
        {
            get { return _SelectedColumnInfo; }
            set
            {
                if (_SelectedColumnInfo != value)
                {
                    _SelectedColumnInfo = value;
                    RaisePropertyChanged(nameof(SelectedColumnInfo));
                }
            }
        }

        private bool _IsColumnWidthVisibile = true;
        public bool IsColumnWidthVisibile
        {
            get { return _IsColumnWidthVisibile; }
            set
            {
                if (_IsColumnWidthVisibile != value)
                {
                    _IsColumnWidthVisibile = value;
                    RaisePropertyChanged(nameof(IsColumnWidthVisibile));
                }
            }
        }

        private int _ColumnCount;
        public int ColumnCount
        {
            get { return _ColumnCount; }
            set
            {
                if (_ColumnCount != value)
                {
                    _ColumnCount = value;
                    RaisePropertyChanged(nameof(ColumnCount));
                }
            }
        }

        private ICommand _DeleteColumnCommand;
        public ICommand DeleteColumnCommand
        {
            get
            {
                if (_DeleteColumnCommand == null)
                    _DeleteColumnCommand = new RelayCommand<ColumnInfoEntity>(p => DeleteColumnExecuted(p), p => DeleteColumnCanExecute(p));
                return _DeleteColumnCommand;
            }
        }

        private ICommand _NewItemAddedCommand;
        public ICommand NewItemAddedCommand
        {
            get
            {
                if (_NewItemAddedCommand == null)
                    _NewItemAddedCommand = new RelayCommand<ColumnInfoEntity>(p => NewItemAddedExecuted(p), p => NewItemAddedCanExecute());
                return _NewItemAddedCommand;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            ColumnInfos = new ObservableCollection<ColumnInfoEntity>()
            {
                new ColumnInfoEntity { Id = 0, ColumnName = "Column 1", ColumnWidth = 30},
                new ColumnInfoEntity { Id = 1, ColumnName = "Column 2", ColumnWidth = 5},
                new ColumnInfoEntity { Id = 2, ColumnName = "Column 3", ColumnWidth = 18},
            };

            ColumnCount = ColumnInfos.Count;

            ColumnInfos.CollectionChanged += ColumnInfos_CollectionChanged;
        }


        private bool DeleteColumnCanExecute(ColumnInfoEntity column)
        {
            return column != null;
        }

        private void DeleteColumnExecuted(ColumnInfoEntity column)
        {
            var message = $"Delete column '{column.ColumnName}'?";
            if (MessageBox.Show(message, "Delete Column", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var columnToDelete = ColumnInfos.FirstOrDefault(x => x.Id == column.Id);

                if (columnToDelete != null)
                {
                    ColumnInfos.Remove(columnToDelete);
                }
                else
                {
                    throw new System.Exception("Column to delete not found");
                }
            }
        }

        private bool NewItemAddedCanExecute()
        {
            return true;
        }

        private void NewItemAddedExecuted(ColumnInfoEntity p)
        {
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ColumnInfos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ColumnCount = ColumnInfos.Count;
        }

        private void DataGrid_AddingNewItem(object sender, System.Windows.Controls.AddingNewItemEventArgs e)
        {

        }
    }
}
