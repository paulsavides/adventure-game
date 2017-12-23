window.onload = init

// will be used to store the canvas context
var ctx;

function init() {
    setupTextArea()


}

function setupTextArea() {
    var textArea = getInputArea()
    textArea.oninput = resizeTextArea
    textArea.onkeydown = captureEnter // prevent new line
    textArea.onkeyup = captureEnterAndProcess // actually prodces input

    if (textArea.value !== '') {
        textArea.style.height = this.scrollHeight + 'px'; // need this because some input could be left
                                                      // over on page reload
    }
}

function resizeTextArea() {
    this.style.height = 'auto'
    this.style.height = this.scrollHeight + 'px';
}

function captureEnter(e) {
    if (isEnter(e)) {
        e.preventDefault()
    }
}

function captureEnterAndProcess(e) {
    if (isEnter(e)) {
        processInput()
    }
}

function processInput() {
    var area = getInputArea()

    var val = area.value
    area.value = ''

    console.log(val)
}

/* utility funcs */
function isEnter(e) {
    return e.keyCode == 13 && e.shiftKey !== true // close enough
}

function getInputArea() {
    return document.getElementById("input-area")
}
