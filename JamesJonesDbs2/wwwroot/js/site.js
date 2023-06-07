//Event Listener to the Switch theme button
//Instanciate stuff once window loads
window.addEventListener('load', (e) => {
    document.getElementById('btnSwitchTheme').addEventListener('click', async (e) => {
        await switchTheme(e);
    });
})
/**
 * Switch Theme method to switch the theme between light and night mode
 * */
async function switchTheme() {
    //retrieve the currently stored theme from local storage
    let currentTheme = localStorage.getItem('theme')

    //if the current theme is 'undefned' : set to light theme by default
    if (currentTheme && currentTheme == 'LightTheme') {

        //update the local storage
        localStorage.setItem('theme', 'NightTheme')

        let result = await fetch('/api/Settings/SetTheme', {
            method: 'POST',
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify({ theme: "NightTheme" })
        })

        console.log(result)
        document.getElementById('themeStyle').setAttribute('href', '/css/Themes/NightTheme.css')
    }
    else {
        //setting the theme to night
        localStorage.setItem('theme', 'LightTheme')

        let result = await advFetch('/api/Settings/SetTheme', {
            method: 'POST',
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify({ theme: "LightTheme" })
        })

        console.log(result)
        document.getElementById('themeStyle').setAttribute('href', '/css/Themes/LightTheme.css')

    }
}

/**
 * Alternate fetch method  to validate anti forgery token
 * */
function advFetch(url, options) {

    let verifyToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

    // define the options object if it has not been defined
    if (options == undefined) {
        options = {};
    }

    // define the headers within options
    if (options.headers == undefined) {
        options.headers = {};
    }

    // set the CSRF token if found
    if (verifyToken != undefined) {
        options.headers['RequestVerificationToken'] = verifyToken;
    }

    // set a custom header to note a fetch request
    options.headers['x-fetch-request'] = "";


    var promise = fetch(url, options)
    return promise;

}
