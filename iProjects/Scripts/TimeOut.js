//
//    // Set timeout variables.
//var timoutWarning = 1200000; // Display warning in 1Mins.
//var timoutNow = 2400000; // Timeout in 2 mins.
//var logoutUrl = '/Account/LockOutScreen'; // URL to logout page.
//
//var warningTimer;
//var timeoutTimer;
//
//// Start timers.
//function StartTimers() {
//    warningTimer = setTimeout("IdleWarning()", timoutWarning);
//    timeoutTimer = setTimeout("IdleTimeout()", timoutNow);
//}
//
//// Reset timers.
//function ResetTimers() {
//    clearTimeout(warningTimer);
//    clearTimeout(timeoutTimer);
//    StartTimers();
////    $("#timeout").dialog('close');
//}
//
//// Show idle timeout warning dialog.
//function IdleWarning() {
//    $("#timeout").dialog({
//        modal: true
//    });
//}
//
//// Logout the user.
//function IdleTimeout() {
//    window.location = logoutUrl;
//}
