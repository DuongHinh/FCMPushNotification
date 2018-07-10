  // Initialize Firebase
  var config = {
    apiKey: "AIzaSyCi9Hb8-N-pn5KCU07enxM7EVQuG3vbCRA",
    authDomain: "mpiweb-1eb6f.firebaseapp.com",
    databaseURL: "https://mpiweb-1eb6f.firebaseio.com",
    projectId: "mpiweb-1eb6f",
    storageBucket: "mpiweb-1eb6f.appspot.com",
    messagingSenderId: "82966767674"
  };
  firebase.initializeApp(config);

  var messaging = firebase.messaging();

  messaging.usePublicVapidKey("BOynhpcxH4QKruvqzjlBLsoA1mpCHBp8DN_5mQLMGt4JhJJMw4RN_HNSUQ_s3n1vy_fc2ahRMvDs2tyJk8B4kBQ");

  messaging.requestPermission().then(function() {
    console.log("ok permission");
     return messaging.getToken();
  }).then(function(token){    
    console.log("get token");
    console.log(token);
  }).catch(function(err) {
    console.log('Permission denied', err);
  });

messaging.onTokenRefresh(function() {
  messaging.getToken().then(function(refreshedToken) {
    console.log('Token refreshed.');
    console.log(refreshedToken);
  }).catch(function(err) {
    console.log('Unable to retrieve refreshed token ', err);
  });
});


messaging.onMessage(function(payload){
console.log('onMessage: ',payload);
});
