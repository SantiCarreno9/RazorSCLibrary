export function isElementVisible(element) {    
    var rect = element.getBoundingClientRect();
    var viewportHeight = window.innerHeight || document.documentElement.clientHeight;

    // Check if the element is fully visible or partially visible
    return (
        rect.top >= 0 &&
        rect.left >= 0 &&
        rect.bottom <= viewportHeight &&
        rect.right <= (window.innerWidth || document.documentElement.clientWidth)
    );
}

export function scrollLeft(container, amount) {
    if (container) {
        const scrollAmount = amount * window.innerWidth;
        const currentPosition = container.scrollLeft;
        const newPosition = currentPosition + scrollAmount;

        container.scrollTo({
            left: newPosition,
            behavior: 'smooth'
        });
        
        return new Promise(resolve => {
            container.addEventListener("scrollend", (event) => {
                resolve(true);
            })
        });
    }
};