function likeButton(el) {
    var ico = document.createElement("img");
    ico.src = "heart.png";
    ico.onclick = function () {
        alert('clico!');
    };
    el.appendChild(ico);
}
