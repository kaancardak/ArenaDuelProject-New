mergeInto(LibraryManager.library, {
    UploadFile: function(gameObjectName, methodName) {
        var gameObjectNameStr = UTF8ToString(gameObjectName);
        var methodNameStr = UTF8ToString(methodName);

        var fileInput = document.createElement('input');
        fileInput.setAttribute('type', 'file');
        fileInput.setAttribute('accept', '.json');
        fileInput.style.display = 'none';

        fileInput.onclick = function (event) {
            this.value = null;
        };

        fileInput.onchange = function (event) {
            var file = event.target.files[0];
            if (file) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    SendMessage(gameObjectNameStr, methodNameStr, e.target.result);
                };
                reader.readAsText(file);
            }
            document.body.removeChild(fileInput);
        }

        document.body.appendChild(fileInput);
        fileInput.click();
    }
});