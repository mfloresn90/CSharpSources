﻿// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved

//
// MainPage.xaml.h
// Declaration of the MainPage.xaml class.
//

#pragma once

#include "pch.h"
#include "MainPage.g.h"

using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::Graphics::Display;
using namespace Windows::UI::ViewManagement;
using namespace Windows::UI::Xaml::Navigation;
using namespace Windows::Foundation;
using namespace Windows::System;
using namespace Platform;
using namespace Windows::UI::Xaml::Interop;

namespace ToastsSampleCPP
{
    public enum class NotifyType
    {
        StatusMessage,
        ErrorMessage
    };
	
	[Windows::Foundation::Metadata::WebHostHidden]
    public ref class MainPage sealed
    {
    public:
        MainPage();

        property Windows::UI::Xaml::Controls::Frame^ ScenariosFrame
        {
            Windows::UI::Xaml::Controls::Frame^ get() { return _scenariosFrame; }
        }
        property Windows::UI::Xaml::Controls::Frame^ InputFrame
        {
            Windows::UI::Xaml::Controls::Frame^ get() { return _inputFrame; }
        }
        property Windows::UI::Xaml::Controls::Frame^ OutputFrame
        {
            Windows::UI::Xaml::Controls::Frame^ get() { return _outputFrame; }
        }

        event Windows::Foundation::EventHandler<Platform::Object^>^ InputFrameLoaded;
        event Windows::Foundation::EventHandler<Platform::Object^>^ OutputFrameLoaded;

        void NotifyUser(String^ strMessage, NotifyType type);
        void DoNavigation(TypeName pageType, Windows::UI::Xaml::Controls::Frame^ frame);

    protected:
        virtual void OnNavigatedTo(Windows::UI::Xaml::Navigation::NavigationEventArgs^ e) override;

    private:
        ~MainPage();

		Windows::UI::Xaml::Controls::Frame^ _scenariosFrame;
        Windows::UI::Xaml::Controls::Frame^ _inputFrame;
        Windows::UI::Xaml::Controls::Frame^ _outputFrame;
        void Footer_Click(Object^ sender, RoutedEventArgs^ e);
        Windows::Foundation::EventRegistrationToken _displayHandlerToken;
        Windows::Foundation::EventRegistrationToken _layoutHandlerToken;
        Windows::Foundation::EventRegistrationToken _pageLoadedHandlerToken;
        Windows::Foundation::EventRegistrationToken _inputFrameLoadedToken;
        Windows::Foundation::EventRegistrationToken _outputFrameLoadedToken;
        void Page_SizeChanged(Platform::Object^ sender, Windows::UI::Core::WindowSizeChangedEventArgs^ e);
        void Page_Loaded(Object^ sender, RoutedEventArgs^ e);
        void CheckLayout();
        void SetFeatureName(Platform::String^ strFeature);
        void inputFrameNavigated(Object^ sender, NavigationEventArgs^ e);
        void outputFrameNavigated(Object^ sender, NavigationEventArgs^ e);

        Platform::String^ _toastContext;
    };

	[Windows::Foundation::Metadata::WebHostHidden]
    public ref class MainPageAndContext sealed
    {
    public:
        MainPageAndContext(ToastsSampleCPP::MainPage^ mainPage, Platform::String^ toastContext)
        {
            _mainPage = mainPage;
            _toastContext = toastContext;
        }
        property ToastsSampleCPP::MainPage^ MainPage
        {
            ToastsSampleCPP::MainPage^ get() { return _mainPage; }
        }
        property Platform::String^ ToastContext
        {
            Platform::String^ get() { return _toastContext; }
        }
    private:
        ToastsSampleCPP::MainPage^ _mainPage;
        Platform::String^ _toastContext;
        
    };
}
