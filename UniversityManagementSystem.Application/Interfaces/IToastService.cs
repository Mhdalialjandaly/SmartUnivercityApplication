using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IToastService
    {
        void ShowInfo(string message, string heading = null, int durationSeconds = 5);

     
        void ShowSuccess(string message, string heading = null, int durationSeconds = 5);

   
        void ShowWarning(string message, string heading = null, int durationSeconds = 5);

     
        void ShowError(string message, string heading = null, int durationSeconds = 5);

      
        void ShowToast(ToastLevel level, string message, string heading = null, int durationSeconds = 5);

        event Action<string, ToastLevel, string, int> OnShow;

 
        event Action OnHideAll;
    }
}
