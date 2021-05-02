function ConfirmDelete() {
    return confirm("Are you sure you want to delete this item?");
}

function PasswordShow() {
    var x = document.getElementById("myPassword");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
} 

function PasswordRepeatedShow() {
    var x = document.getElementById("myPasswordRepeated");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
} 