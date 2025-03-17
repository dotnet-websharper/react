import { build } from 'esbuild'

var options =
{
  entryPoints: ['./build/WebSharper.React.Tests.js'],
  bundle: true,
  minify: true,
  format: 'iife',
  outfile: './Content/WebSharper.React.Tests.min.js',
  globalName: 'wsbundle'
};

build(options);

