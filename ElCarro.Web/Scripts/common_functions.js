var closeMsg = function (className) {
    var x = document.getElementsByClassName(className)[0];
    if (x) {
        x.parentNode.removeChild(x);
    }
}
