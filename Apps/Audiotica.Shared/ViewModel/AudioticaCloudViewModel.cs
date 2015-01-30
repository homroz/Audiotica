﻿using Audiotica.Core.WinRt.Common;
using Audiotica.Data.Service.Interfaces;
using Audiotica.Data.Service.RunTime;
using Audiotica.PartialView;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Audiotica.ViewModel
{
    public class AudioticaCloudViewModel : ViewModelBase
    {
        public AudioticaCloudViewModel(IAudioticaService service)
        {
            Service = service;
            SignInCommand = new RelayCommand(SignInExecute);
            SignUpCommand = new RelayCommand(SignUpExecute);
        }

        public IAudioticaService Service { get; private set; }
        public RelayCommand SignInCommand { get; set; }
        public RelayCommand SignUpCommand { get; set; }

        private async void SignInExecute()
        {
            var signInSheet = new EmailSignInSheet();
            var success = await ModalSheetUtility.ShowAsync(signInSheet);

            if (success)
                CurtainPrompt.Show("Welcome!");
        }

        private async void SignUpExecute()
        {
            var signInSheet = new EmailSignUpSheet();
            var success = await ModalSheetUtility.ShowAsync(signInSheet);

            if (success)
                CurtainPrompt.Show("Welcome!");
        }
    }
}