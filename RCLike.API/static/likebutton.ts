
function likeButton(el: HTMLDivElement): void {
    const ico = document.createElement("img");
    ico.src = "heart.png";
    ico.onclick = () => {
        alert('clico!');
    };
    el.appendChild(ico);
}