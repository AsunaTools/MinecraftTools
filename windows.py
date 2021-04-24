import getpass
import os
import shutil
import platform
import time
import urllib.request
import progressbar
from zipfile import ZipFile

os.system('cls')
print('==========================================================================')
time.sleep(0.1)
print('       d8888                                     888b     d888  .d8888b.  ')
time.sleep(0.1)
print('      d88888                                     8888b   d8888 d88P  Y88b ')
time.sleep(0.1)
print('     d88P888                                     88888b.d88888 888    888 ')
time.sleep(0.1)
print('    d88P 888 .d8888b  888  888 88888b.   8888b.  888Y88888P888 888        ')
time.sleep(0.1)
print('   d88P  888 88K      888  888 888 "88b     "88b 888 Y888P 888 888        ')
time.sleep(0.1)
print('  d88P   888 "Y8888b. 888  888 888  888 .d888888 888  Y8P  888 888    888 ')
time.sleep(0.1)
print(' d8888888888      X88 Y88b 888 888  888 888  888 888   "   888 Y88b  d88P ')
time.sleep(0.1)
print('d88P     888  88888P   "Y88888 888  888 "Y888888 888       888  "Y8888P"  ')
time.sleep(0.1)
print('==========================================================================')
time.sleep(0.5)
print('')
time.sleep(1)
print('Sys:' + platform.system())
print('')
User = getpass.getuser()

MinecraftDir = 'C:/Users/' + User + '/AppData/Roaming/.minecraft'
MinecraftDirExist = os.path.isdir(MinecraftDir)

ModsDir = MinecraftDir + '/mods'
ModsDirExist = os.path.isdir(ModsDir)
ModZip = ModsDir + '/mods.zip'

if not MinecraftDirExist:
    print('Minecraft no esta instalado o no se encuentra en su posicion por defecto!')
    exit(1)
if not ModsDirExist:
    print('Creando una ubicacion para alojar los mods...')
    print('')
    os.mkdir(ModsDir)

for filename in os.listdir(ModsDir):
    file_path = os.path.join(ModsDir, filename)
    try:
        if os.path.isfile(file_path) or os.path.islink(file_path):
            os.unlink(file_path)
        elif os.path.isdir(file_path):
            shutil.rmtree(file_path)
    except Exception as e:
        print('¡Vaya! %s. Reason: %s' % (file_path, e))
        exit(1)

time.sleep(2)

url = 'http://asuna.tools/data/downloads/mods.zip'

pbar = None


def show_progress(block_num, block_size, total_size):
    global pbar
    if pbar is None:
        pbar = progressbar.ProgressBar(maxval=total_size)
        pbar.start()

    loaded = block_num * block_size
    if loaded < total_size:
        pbar.update(loaded)
    else:
        pbar.finish()
        pbar = None


print('## Descargando mods.zip...')
print('')

urllib.request.urlretrieve(url, ModZip, show_progress)
a = 1
print('')
print('## Extrayendo mods.zip...')
print('')
with ZipFile(ModZip, 'r') as zipObj:
    zipObj.extractall(ModsDir)
print('100% |########################################################################|')
print('')
print('Eliminando archivos innecesarios...')
print('')
os.remove(ModZip)
print('100% |########################################################################|')
print('')
print('¡Hecho! Gracias por usar AsunaTools una vez mas,')
print('Escrito por Asuna y Ekardo')
print('')
os.system("pause")
