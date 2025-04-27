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

export function scrollLeft(container, amount, smooth) {
    if (container) {
        const scrollAmount = amount * container.clientWidth;
        const currentPosition = container.scrollLeft;
        const newPosition = currentPosition + scrollAmount;

        let behavior = 'smooth';
        if (!smooth) behavior = 'instant'

        container.scrollTo({
            left: newPosition,
            behavior: behavior
        });

        return new Promise(resolve => {
            container.addEventListener("scrollend", (event) => {
                resolve(true);
            })
        });
    }
};

export function muteVideo(element) {
    if (element == null)
        return;
    try {
        element.muted = true;
    } catch (error) {
        console.error(error);
    }

}

export function pauseVideo(element) {
    if (element == null)
        return;
    try {
        if (element != null && !element.paused)
            element.pause();
    } catch (error) {
        console.error(error);
    }
}

export function playVideo(element) {
    if (element == null)
        return;
    try {
        element.play();
    } catch (error) {
        console.error(error);
    }
}

export function stopWebVideo(iframe) {
    if (iframe == null)
        return;
    try {
        let iframeSrc = iframe.src;
        iframe.src = iframeSrc;
    }
    catch (error) {
        console.error(error);
    }
}