/*
document.addEventListener("DOMContentLoaded", function() {
    $(".image-input").on("change", function () {
        if (window.FormData === undefined) {
            alert('В вашем браузере FormData не поддерживается')
        } else {

            let formData = new FormData();
            let imageBytes = ($(".image-input")[0].files[0]);
            formData.append('file', imageBytes);
            console.log(formData);
        }
    });
});
document.addEventListener("DOMContentLoaded", function() {
    $(".add-button").on("click", function() {

        let mainElementId = $(this).closest("form").attr("id");

        let formData = new FormData($("#" + mainElementId)[0]);
        // formData.append("mainElementId", mainElementId);

/!*        let fileInput = $("#" + mainElementId + " .image-input")[0];
        let imageFile = fileInput.files[0];*!/

/!*        if (imageFile) {
            formData.append("imageData", imageFile)
            let reader = new FileReader();
            reader.onload = function (e) {
                formData.append("imageData", e.target.result);
            }
        }
        console.log(formData);*!/

        console.log(formData);
        formData.append("mainElementId", mainElementId);


        $.ajax({
            type: "POST",
            url: '/storeInfo/storeInfoInDataBase',
            cache: false,
            contentType: false,
            processData: false,
            data: formData,
            dataType: 'json',
            success: function (response) {
                console.log("Данные успешно отправлены на сервер");

            },
            error: function (error) {
                console.error("Произошла ошибка при отправке данных на сервер");
            }
        });
    });
});*/



/*document.addEventListener("DOMContentLoaded", function() {
    $(".add-button").on("click", function(element) {

        let file = $(".add-button").siblings()[1].files[0];

        let mainElementId = $(this).closest("form").attr("id");
        console.log(mainElementId);

        let formData = new FormData($("#" + mainElementId)[0]);
        formData.append("mainElementId", mainElementId);

        if (file) {
            formData.append("imageData", file)
            let reader = new FileReader();
            reader.onload = function (e) {
                formData.append("imageData", e.target.result);
                reader.readAsDataURL(file);
            }
        }


/!*        let textValue = $(this).siblings()[0].value;
        let imageValue = $(this).siblings()[1].files[0];
        let pasteTextID = mainElementId + "-next";
        console.log("imageValue:")
        console.log({imageValue});*!/

        $.ajax({
            dataType : "json",
            url: "/storeInfo/storeInfoInDataBase",
            type: "POST",
            data: formData,
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            processData: false,
            success: function (response) {
                console.log("Данные успешно отправлены на сервер");
/!*                CreateParagraph(textValue, imageValue , pasteTextID, element);*!/

            },
            error: function (error) {
                console.error("Произошла ошибка при отправке данных на сервер");

            }
        });
    });
});*/

document.addEventListener("DOMContentLoaded", function() {
    $(".add-button").on("click", function(element) {
        let file = $(".add-button").siblings()[1].files[0];
        let textValue = $(this).siblings()[0].value;

        let mainElementId = $(this).closest("form").attr("id");
        console.log(mainElementId);

        let formData = new FormData($("#" + mainElementId)[0]);
        formData.append("mainElementId", mainElementId);


        if (file) {
            let byteArrayFromFile = [];
            let reader = new FileReader();
            reader.onload = function (e) {
                let data = e.target.result;
/*                let byteData = new BigUint64Array(data);
                for (let i=0; i< byteData.length; i++){
                    byteArrayFromFile.push(byteData[i]);
                }*/
                formData.append("imageData", data);
                let info = {
                    text: textValue,
                    elementId: mainElementId,
                    image: data
                }
                sendAjaxRequest(info);
            };
            reader.readAsDataURL(file);
        } else {
            let info = {
                text: textValue,
                elementId: mainElementId,
            }
            sendAjaxRequest(info);
        }
    });

    function sendAjaxRequest(formData) {
        $.ajax({
            url: "/storeInfo/storeInfoInDataBase",
            type: "POST",
            dataType: 'json',
            data: JSON.stringify(formData),
            contentType: false,
            processData: false,
            success: function (response) {
                console.log("Данные успешно отправлены на сервер");
            },
            error: function (error) {
                console.error("Произошла ошибка при отправке данных на сервер");
            }
        });
    }
});

function CreateParagraph(text = null, image = null, elementID, element ){
    if(text){
        let paragraph = document.createElement("p");
        paragraph.innerHTML = text;
        document.getElementById(elementID).appendChild(paragraph);
    }
    if(image){
        let reader = new FileReader();

        let imageElement = document.createElement("img");

        reader.onload = function() {
            console.log(imageElement.src);
            imageElement.src = element.target.result ;
            console.log(imageElement.src);
        }

        reader.readAsDataURL(image);
        document.getElementById(elementID).appendChild(imageElement);
    }
}

//nooooooooooooooooooorm
/*
        var fileReader = new FileReader();
        fileReader.onload = function() {
            imageElement.src = fileReader.result;
        }
        fileReader.readAsDataURL(imageElement.files[0]);

document.addEventListener("DOMContentLoaded", function() {
    $(".add-button").on("click", function() {

        let mainElementId = $(this).closest("form").attr("id");

        let formData = new FormData($("#" + mainElementId)[0]);
       // formData.append("mainElementId", mainElementId);

        let fileInput = $("#" + mainElementId + " .image-input")[0];
        let imageFile = fileInput.files[0];

        if (imageFile) {
            formData.append("imageData", imageFile)
            let reader = new FileReader();
            reader.onload = function (e) {
                formData.append("imageData", e.target.result);
            }
        }
        console.log(formData);

        $.ajax({
            url: "/storeInfo/storeInfoInDataBase",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                console.log("Данные успешно отправлены на сервер");
            },
            error: function (error) {
                console.error("Произошла ошибка при отправке данных на сервер");
            }
        });
    });
});

*/











/*
let username = document.getElementById("username");
let password = document.getElementById("password");
let login = document.getElementById("login");
document.getElementById("sign-in").onclick = function () {
    let hasError = false;

    [username, password, login].forEach(item => {
        if (!item.value) {
            item.style.borderColor = "red" ;
            hasError = true;
        }
        else if (item.value === " "){
            hasError = true;
            alert("Вводите корректные данные!")
        }
        else {
            item.parentElement.style.background = "";
        }
    });

    if (!hasError) {
        [username, password, login].forEach(item => {
            item.value = "";
        });
    }
}


let event = 'load'; // hover, keydown ..
var getImage = function( selectedID ) { $('#depIcon').html('<img src="php/getDepImage.php?id='+selectedID+'" alt="">'); }

$('#trigger').on(event, getImage( selectedID ));*/
