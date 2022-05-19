export const getContentType = (url: string) => {
    return new Promise((resolve, reject) => {
        var req = new XMLHttpRequest();
        req.open("GET", url, true);
        req.setRequestHeader("Range", "bytes=0");
        req.onreadystatechange = () => {
            if (req.readyState === req.DONE) {
                resolve(req.getResponseHeader("Content-Type"));
            }
        };
        req.send();
    });
}