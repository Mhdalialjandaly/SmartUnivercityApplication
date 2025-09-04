using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Services
{
    public class ToastService : IToastService
    {
        public event Action<string, ToastLevel, string, int> OnShow = delegate { };
        public event Action OnHideAll = delegate { };

        public void ShowInfo(string message, string heading = null, int durationSeconds = 5)
        {
            ShowToast(ToastLevel.Info, message, heading, durationSeconds);
        }

        public void ShowSuccess(string message, string heading = null, int durationSeconds = 5)
        {
            ShowToast(ToastLevel.Success, message, heading, durationSeconds);
        }

        public void ShowWarning(string message, string heading = null, int durationSeconds = 5)
        {
            ShowToast(ToastLevel.Warning, message, heading, durationSeconds);
        }

        public void ShowError(string message, string heading = null, int durationSeconds = 5)
        {
            ShowToast(ToastLevel.Error, message, heading, durationSeconds);
        }

        public void ShowToast(ToastLevel level, string message, string heading = null, int durationSeconds = 5)
        {
            OnShow(message, level, heading ?? string.Empty, durationSeconds);
        }

        public void HideAll()
        {
            OnHideAll();
        }
    }
}