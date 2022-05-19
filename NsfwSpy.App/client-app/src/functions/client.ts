export const uploadGif = (file: Blob) => {
    return uploadFile("/nsfwspy/gif", file);
}

export const uploadImage = (file: Blob) => {
    return uploadFile("/nsfwspy/image", file);
}

export const uploadVideo = (file: Blob) => {
    return uploadFile("/nsfwspy/video", file);
}

export const getMediaInfo = (url: string) => {
    return fetch(`/nsfwspy/url/${encodeURIComponent(url)}`);
}

const uploadFile = (url: string, file: Blob) => {
    var postSettings: RequestInit = {
        method: 'POST',
        mode: 'cors'
    };
    var data = new FormData();
    data.append('file', file);
    postSettings.body = data;

    return fetch(url, postSettings);
}