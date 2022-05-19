import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faImage, faVideo } from '@fortawesome/free-solid-svg-icons'
import { Logo } from './components/Logo/Logo';
import { selectFiles } from './functions/selectFiles';
import { ImageFile } from './models/ImageFile';
import { getMediaInfo, uploadGif, uploadImage, uploadVideo } from './functions/client';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import './App.scss';
import { NsfwSpyFramesResult } from './models/NsfwSpyFramesResult';
import { NsfwSpyResult } from './models/NsfwSpyResult';
import { sortNsfwResult } from './functions/sortBy';
import { getContentType } from './functions/getContentType';
import { MediaInfo } from './models/MediaInfo';

export const App: React.FC = () => {
    const [url, setUrl] = useState<string>();
    const [image, setImage] = useState<ImageFile>();
    const [video, setVideo] = useState<ImageFile>();
    const [imageResults, setImageResults] = useState<NsfwSpyResult>();
    const [videoResults, setVideoResults] = useState<NsfwSpyFramesResult>();
    const [processing, setProcessing] = useState<boolean>(false);

    useEffect(() => {
        if (!url) return;

        const handleUrl = async () => {
            const result = await getMediaInfo(url);
            const data = await result.blob()
            handleFile(data);
        }

        handleUrl();
    }, [url])

    const selectFile = () => {
        selectFiles({ accept: 'image/*;video/*', multiple: false }).then(async files => {
            if (files) {
                handleFile(files[0])
            }
        });
    }

    const handleFile = async (file: Blob) => {
        const imageFile: ImageFile = {
            file: file,
            url: URL.createObjectURL(file)
        };

        setImage(undefined);
        setVideo(undefined);
        setImageResults(undefined);
        setVideoResults(undefined);

        const fileType = imageFile.file.type;

        setProcessing(true);
        if (fileType === "image/gif") {
            setImage(imageFile);
            const result = await uploadGif(imageFile.file);
            const data: NsfwSpyFramesResult = await result.json();
            setVideoResults(data);
        } else if (fileType.startsWith("image/")) {
            setImage(imageFile);
            const result = await uploadImage(imageFile.file);
            const data: NsfwSpyResult = await result.json();
            setImageResults(data);
        } else if (fileType.startsWith("video/")) {
            setVideo(imageFile);
            const result = await uploadVideo(imageFile.file);
            const data: NsfwSpyFramesResult = await result.json();
            setVideoResults(data);
        }
        setProcessing(false);
    }

    let sortedImageResults: [string, any][] | undefined = undefined;
    if (imageResults) {
        sortedImageResults = sortNsfwResult(imageResults);
    }

    return (
        <div className="app">
            <header>
                <Logo />
            </header>
            <main>
                <section className="image-section">
                    <div className="image-canvas" onClick={selectFile}>
                        {!image && !video &&
                            <>
                                <div>
                                    Select an image, Gif or video.
                                </div>
                                <div className="icons">
                                    <div><FontAwesomeIcon icon={faImage} /></div>
                                    <div><FontAwesomeIcon icon={faVideo} /></div>
                                </div>
                                <div className="subtitle">
                                    Or paste a link below...
                                </div>
                            </>}
                        {image &&
                            <img src={image.url} className="image-preview" />}
                        {video &&
                            <video
                                src={video.url}
                                autoPlay
                                loop
                                muted
                                className="video-preview" />}
                    </div>
                    <input
                        type="text"
                        placeholder="https://i3.ytimg.com/vi/dQw4w9WgXcQ/maxresdefault.jpg"
                        onChange={(e) => setUrl(e.target.value)} />
                </section>
                <section className="results-section">
                    {processing &&
                        <div>
                            Processing...
                        </div>}
                    {videoResults &&
                        <>
                            <ResponsiveContainer height="100%" width="100%">
                                <LineChart
                                    width={500}
                                    height={300}
                                    data={Object.values(videoResults.frames)}
                                    margin={{
                                        top: 5,
                                        right: 30,
                                        left: 20,
                                        bottom: 5,
                                    }}>
                                    <XAxis dataKey="name" />
                                    <YAxis />
                                    <Tooltip contentStyle={{ background: "#292929" }} />
                                    <Legend />
                                    <Line type="monotone" dataKey="hentai" stroke="#c25452" strokeWidth={3} dot={false} />
                                    <Line type="monotone" dataKey="neutral" stroke="#ffffff" strokeWidth={3} dot={false} />
                                    <Line type="monotone" dataKey="pornography" stroke="#ffa31a" strokeWidth={3} dot={false} />
                                    <Line type="monotone" dataKey="sexy" stroke="#fdfd66" strokeWidth={3} dot={false} />
                                </LineChart>
                            </ResponsiveContainer>
                            <span>{videoResults.frameCount} Frames Analysed</span>
                        </>}
                    {sortedImageResults &&
                        <div>
                            {sortedImageResults.map((result) =>
                                <div className={`result-value ${result[0]}`}>
                                    <span>{result[0]}</span>
                                    <span>{result[1]}</span>
                                </div>
                            )}
                        </div>}
                </section>
            </main >
        </div >
    );
}

export default App;
