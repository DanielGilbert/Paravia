window.BlazorConsole = {
    scrollToBottom: function () {
        window.scrollTo(0, document.body.scrollHeight);
    },
    setFocusToElement: function (elementId) {
        const element = document.getElementById(elementId);
        element.focus();
    },
    readLine: function (msg) {
        input = window.prompt(msg);
        return input;
    }
};
