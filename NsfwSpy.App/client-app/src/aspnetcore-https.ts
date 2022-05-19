import * as fs from 'fs'
import * as path from 'path'
import * as child_process from 'child_process'

const spawn = child_process.spawn;

const baseFolder = process.env.APPDATA !== undefined && process.env.APPDATA !== ''
    ? `${process.env.APPDATA}/ASP.NET/https`
    : `${process.env.HOME}/.aspnet/https`;

const certArg = process.argv.map(arg => arg.match('/--name=(?<value>.+)/i')).filter(Boolean)[0];
const certName = certArg ? certArg?.groups?.value : process.env.npm_package_name;

if (!certName) {
    console.error('Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly');
    process.exit(-1);
}

const moduleCertFilePath = path.join(baseFolder, `${certName}.pem`);
const moduleKeyFilePath = path.join(baseFolder, `${certName}.key`);

if (!fs.existsSync(moduleCertFilePath) || !fs.existsSync(moduleKeyFilePath)) {
    spawn('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        moduleCertFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', })
        .on('exit', (code: any) => process.exit(code));
}