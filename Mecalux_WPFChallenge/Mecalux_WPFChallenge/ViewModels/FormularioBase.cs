using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecalux_WPFChallenge.ViewModels
{
    public class FormularioBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _erroresPorPropiedad =
            new Dictionary<string, List<string>>();

        public bool HasErrors => _erroresPorPropiedad.Values.Any(list => list.Count > 0);

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable<string> GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return _erroresPorPropiedad.Values.SelectMany(messages => messages);

            if (!_erroresPorPropiedad.TryGetValue(propertyName, out var list) || list.Count == 0)
                return Enumerable.Empty<string>();

            return list;
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_erroresPorPropiedad.ContainsKey(propertyName))
                _erroresPorPropiedad[propertyName] = new List<string>();

            if (_erroresPorPropiedad[propertyName].Contains(error))
                return;

            _erroresPorPropiedad[propertyName].Add(error);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
            OnValidationChanged();
        }

        protected void ClearErrors(string propertyName)
        {
            if (!_erroresPorPropiedad.Remove(propertyName))
                return;

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
            OnValidationChanged();
        }

        protected virtual void OnValidationChanged() { }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return GetErrors(propertyName);
        }
    }
}
