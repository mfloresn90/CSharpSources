﻿//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

(function () {
    "use strict";

    var urlBox, webImageContainer, loadingElem, loadingText, timeout;
    var altText = "Web image";

    var page = WinJS.UI.Pages.define("/html/scenario3.html", {
        ready: function (element, options) {
            // Get commonly used elements
            urlBox = document.getElementById("imageSrcInput");
            webImageContainer = document.getElementById("webImage");
            loadingElem = document.getElementById("loading");
            loadingText = document.getElementById("imageText");

            // Update the image when the user finishes typing, or when "Enter" is pressed.
            urlBox.addEventListener("input", updateImage, false);

            webImageContainer.addEventListener("load", function () {
                webImageContainer.style.display = "inherit";
                webImageContainer.style.opacity = 1.0;
                loadingElem.style.display = "none";
            }, false);

            webImageContainer.addEventListener("error", function () {
                loadingElem.style.display = "none";
                loadingText.innerText = "Cannot load the image.";
            }, false);


            // Assign button click events
            var buttons = document.querySelectorAll("button.toastButton");
            for (var i = 0, len = buttons.length; i < len; i++) {
                buttons[i].addEventListener("click", displayWebImageToast, false);
            }

            buttons = document.querySelectorAll("button.toastStringButton");
            for (i = 0, len = buttons.length; i < len; i++) {
                buttons[i].addEventListener("click", displayWebImageToastWithStringManipulation, false);
            }
        }
    });

    function updateImage() {
        clearTimeout(timeout);
        webImageContainer.style.opacity = 0.5;
        loadingElem.style.display = "block";
        loadingText.innerText = "";
        timeout = setTimeout(/*@static_cast(Object)*/function () {
            webImageContainer.style.display = "none";
            webImageContainer.src = urlBox.value;
        }, 500);
    };

    var Notifications = Windows.UI.Notifications;
    var ToastContent = NotificationsExtensions.ToastContent;

    function displayWebImageToast(e) {
        var targetButton = e.currentTarget;
        var templateName = targetButton.id;

        // Get the toast manager for the current app.
        var notificationManager = Notifications.ToastNotificationManager;

        // Create the toast content using the notifications content library.
        var content;

        if (templateName === "toastImageAndText01") {
            content = ToastContent.ToastContentFactory.createToastImageAndText01();
            content.textBodyWrap.text = "Body text that wraps over three lines";
        } else if (templateName === "toastImageAndText02") {
            content = ToastContent.ToastContentFactory.createToastImageAndText02();
            content.textHeading.text = "Heading text";
            content.textBodyWrap.text = "Body text that wraps over two lines";
        } else if (templateName === "toastImageAndText03") {
            content = ToastContent.ToastContentFactory.createToastImageAndText03();
            content.textHeadingWrap.text = "Heading text that wraps over two lines";
            content.textBody.text = "Body text";
        } else if (templateName === "toastImageAndText04") {
            content = ToastContent.ToastContentFactory.createToastImageAndText04();
            content.textHeading.text = "Heading text";
            content.textBody1.text = "First body text";
            content.textBody2.text = "Second body text";
        }

        content.image.src = urlBox.value;
        content.image.alt = altText;

        // Display the XML of the toast.
        WinJS.log && WinJS.log(content.getContent(), "sample", "status");

        // Create a toast, then create a ToastNotifier object
        // to send the toast.
        var toast = content.createNotification();

        notificationManager.createToastNotifier().show(toast);
    }

    function displayWebImageToastWithStringManipulation(e) {
        var targetButton = e.currentTarget;
        var templateName = targetButton.id.substring(0, 19);

        // Get the toast manager for the current app.
        var notificationManager = Notifications.ToastNotificationManager;

        var toastXmlString;

        if (templateName === "toastImageAndText01") {
            toastXmlString = "<toast>"
                           + "<visual version='1'>"
                           + "<binding template='toastImageAndText01'>"
                           + "<text id='1'>Body text that wraps over three lines</text>"
                           + "<image id='1' src='" + urlBox.value + "' alt='" + altText + "'/>"
                           + "</binding>"
                           + "</visual>"
                           + "</toast>";
        } else if (templateName === "toastImageAndText02") {
            toastXmlString = "<toast>"
                           + "<visual version='1'>"
                           + "<binding template='toastImageAndText02'>"
                           + "<text id='1'>Heading text</text>"
                           + "<text id='2'>Body text that wraps over two lines</text>"
                           + "<image id='1' src='" + urlBox.value + "' alt='" + altText + "'/>"
                           + "</binding>"
                           + "</visual>"
                           + "</toast>";
        } else if (templateName === "toastImageAndText03") {
            toastXmlString = "<toast>"
                           + "<visual version='1'>"
                           + "<binding template='toastImageAndText03'>"
                           + "<text id='1'>Heading text that wraps over two lines</text>"
                           + "<text id='2'>Body text</text>"
                           + "<image id='1' src='" + urlBox.value + "' alt='" + altText + "'/>"
                           + "</binding>"
                           + "</visual>"
                           + "</toast>";
        } else if (templateName === "toastImageAndText04") {
            toastXmlString = "<toast>"
                           + "<visual version='1'>"
                           + "<binding template='toastImageAndText04'>"
                           + "<text id='1'>Heading text</text>"
                           + "<text id='2'>First body text</text>"
                           + "<text id='3'>Second body text</text>"
                           + "<image id='1' src='" + urlBox.value + "' alt='" + altText + "'/>"
                           + "</binding>"
                           + "</visual>"
                           + "</toast>";
        }

        // Create a toast, then create a ToastNotifier object
        // to send the toast.
        var toastDOM = new Windows.Data.Xml.Dom.XmlDocument();
        try {
            toastDOM.loadXml(toastXmlString);
            var toast = new Notifications.ToastNotification(toastDOM);

            // Display the XML of the toast.
            WinJS.log && WinJS.log(toastDOM.getXml(), "sample", "status");

            notificationManager.createToastNotifier().show(toast);
        } catch (e) {
            WinJS.log && WinJS.log("Error loading the xml, check for invalid characters in the input", "sample", "error");
        }
    }
})();
