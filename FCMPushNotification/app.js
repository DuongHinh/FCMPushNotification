var config = {
    apiKey: "AIzaSyB_cjRV0s1rStUAX-1DQtczx8OTAIyJq_0",
    authDomain: "hinhdx-533aa.firebaseapp.com",
    databaseURL: "https://hinhdx-533aa.firebaseio.com",
    projectId: "hinhdx-533aa",
    storageBucket: "hinhdx-533aa.appspot.com",
    messagingSenderId: "128976040577"
};


firebase.initializeApp(config);

// Retrieve Firebase Messaging object.
var messaging = firebase.messaging();

// Add the public key generated from the console here.
messaging.usePublicVapidKey("BOF1Zo_js1NYkY_cvneGSUxQPFOFbxb_brYj2s2kBOPrD6DIOnH3sMkpFZnJvGeJmzNscXTzLihdIZ4uVSywqpM");

if ('serviceWorker' in navigator) {
    navigator.serviceWorker.register('/service-worker.js').then(function (registration) {
        // Registration was successful
        messaging.useServiceWorker(registration);

        console.log('ServiceWorker registration successful with scope: ', registration.scope);
    }).catch(function (err) {
        // registration failed :(
        console.log('ServiceWorker registration failed: ', err);
    });
}

messaging.requestPermission().then(function () {
    console.log('Have permission');
    messaging.getToken().then(function (currentToken) {
        if (currentToken) {
            console.log(currentToken);
        } else {
            // Show permission request.
            console.log('No Instance ID token available. Request permission to generate one.');

        }
    });
}).catch(function (err) {
    console.log('Error Ocured', err);
});