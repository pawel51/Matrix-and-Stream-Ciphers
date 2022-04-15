using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Ciphers
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _Message = "";

        public string Message
        {
            get { return _Message; }
            set { _Message = value; OnPropertyChanged(nameof(Message)); }
        }

        private string _Encrypted = "";

        public string Encrypted
        {
            get { return _Encrypted; }
            set { _Encrypted = value; OnPropertyChanged(nameof(Encrypted)); }
        }

        private string _Decrypted = "";

        public string Decrypted
        {
            get { return _Decrypted; }
            set { _Decrypted = value; OnPropertyChanged(nameof(Decrypted)); }
        }

        private string _ToDecrypt = "";
        public string ToDecrypt
        {
            get { return _ToDecrypt; }
            set { _ToDecrypt = value; OnPropertyChanged(nameof(ToDecrypt)); }
        }

        private string _Key = "";

        public string Key
        {
            get { return _Key; }
            set { _Key = value; OnPropertyChanged(nameof(Key)); }
        }

        private string _n;

        public string n
        {
            get { return _n; }
            set { _n = value; OnPropertyChanged(nameof(n)); }
        }

        private Visibility _VisibilityKey;

        public Visibility VisibilityKey
        {
            get { return _VisibilityKey; }
            set { _VisibilityKey = value; OnPropertyChanged(nameof(VisibilityKey)); }
        }

        private Visibility _VisibilityHeight;

        public Visibility VisibilityHeight
        {
            get { return _VisibilityHeight; }
            set { _VisibilityHeight = value; OnPropertyChanged(nameof(VisibilityHeight)); }
        }

        private AlgorithmsEnum _SelectedAlgorithm;

        public AlgorithmsEnum SelectedAlgorithm
        {
            get { return _SelectedAlgorithm; }
            set { _SelectedAlgorithm = value; OnPropertyChanged(nameof(SelectedAlgorithm)); }
        }


        private List<AlgorithmsEnum> _Algorithms = new List<AlgorithmsEnum>() { AlgorithmsEnum.zad1, AlgorithmsEnum.zad2a, AlgorithmsEnum.zad2b };

        public List<AlgorithmsEnum> Algorithms
        {
            get { return _Algorithms; }
            set { _Algorithms = value; OnPropertyChanged(nameof(Algorithms)); }
        }

        private string _input;

        public string Input
        {
            get { return _input; }
            set { _input = value; OnPropertyChanged(nameof(Input)); }
        }

        private string _seed;

        public string Seed
        {
            get { return _seed; }
            set { _seed = value; OnPropertyChanged(nameof(Seed)); }
        }

        private string _output;

        public string Output
        {
            get { return _output; }
            set { _output = value; OnPropertyChanged(nameof(Output)); }
        }

        private List<string> _degrees = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public List<string>  Degrees
        {
            get { return _degrees; }
            set { _degrees = value; }
        }

        private string _selectedDegree = "4";

        public string SelectedDegree
        {
            get { return _selectedDegree; }
            set { _selectedDegree = value; OnPropertyChanged(nameof(SelectedDegree)); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
