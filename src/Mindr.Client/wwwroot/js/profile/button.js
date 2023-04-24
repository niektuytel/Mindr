export function clickOutsideHandler(buttonId, dotnetHelper) {
    const button = document.getElementById(buttonId);

    document.addEventListener('click', (e) => {
        if (e && e.target !== button && button.hasAttribute('aria-expanded') && !button.contains(e.target)) {
            dotnetHelper.invokeMethodAsync("HideMenu");
        }
    });
}
