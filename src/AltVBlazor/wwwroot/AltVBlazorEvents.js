window.emitClient = (eventName, ...args) => {
    if (!('alt' in window)) {
        console.log(`[AltVBlazorEvents] emit ${eventName}(${args})`);
        return;
    }
    alt.emit(eventName, ...args);
}

window.registerCallback = (eventName, objReference, callbackName) => {
    if ('alt' in window) {
        alt.on(eventName, async (...args) => {
            await objReference.invokeMethodAsync(callbackName, ...args);
        });
        return;
    }

    window.addEventListener(eventName, async (eventData) => {
        await objReference.invokeMethodAsync(callbackName, ...eventData.detail);
    });
}