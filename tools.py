import os
import sys
import time
import shutil
import getpass
import progressbar
import urllib.request
from zipfile import ZipFile

sys = sys.platform
if sys not in ['win32', 'linux', 'darwin']:
    print('The system is not supported!')
    print('')
    exit(1)


def clear():
    os.system('cls' if sys == 'win32' else 'clear')


def get_minecraft_dir():
    global minecraft_dir
    user = getpass.getuser()

    if sys == 'win32':
        minecraft_dir = 'C:/Users/' + user + '/AppData/Roaming/.minecraft'
    elif sys == 'linux':
        minecraft_dir = '/home/' + user + '/.minecraft'
    elif sys == 'darwin':
        minecraft_dir = '/Users/' + user + '/Library/Application Support/minecraft'

    if not os.path.isdir(minecraft_dir):
        print('Minecraft no esta instalado o no se encuentra en su posicion por defecto!')
        exit(1)


clear()
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
time.sleep(1)
clear()


def menu():
    print('===================================')
    print('          Minecraft Tools          ')
    print('===================================')
    print('')
    print('(1) Instalar Forge')
    print('(2) Instalar mods')
    print('(3) Instalar OptiFine (Version)')
    print('')
    print('(q) Salir')
    action = input('Selecciona una opcion: ')

    if action == '1':
        forge()
    elif action == '2':
        mods()
    elif action == '3':
        optifine()
    elif action == 'q':
        clear()
        print('===================================')
        print('          Minecraft Tools          ')
        print('===================================')
        print('')
        print('¡Hecho! Gracias por usar AsunaTools una vez mas,')
        print('Escrito por Asuna y Ekardo')
        print('')
        if sys == 'win32':
            os.system('pause')
        exit(0)
    else:
        clear()
        menu()


def forge():
    clear()
    get_minecraft_dir()

    versions_dir = minecraft_dir + '/versions'
    forge_zip = versions_dir + '/forge.zip'

    time.sleep(2)

    url = 'https://asuna.tools/data/downloads/asunamc/forge.zip'

    def show_progress(block_num, block_size, total_size):
        global pbar
        pbar = progressbar.ProgressBar(maxval=total_size)
        pbar.start()

        loaded = block_num * block_size
        if loaded < total_size:
            pbar.update(loaded)
        else:
            pbar.finish()
            pbar = None

    print('## Descargando Forge...')
    print('')

    urllib.request.urlretrieve(url, forge_zip, show_progress)
    a = 1
    print('')
    print('## Extrayendo Forge...')
    print('')
    with ZipFile(forge_zip, 'r') as zipObj:
        zipObj.extractall(versions_dir)
    print('100% |########################################################################|')
    print('')
    print('Eliminando archivos innecesarios...')
    print('')
    os.remove(forge_zip)
    print('100% |########################################################################|')
    time.sleep(1)
    clear()
    menu()


def mods():
    clear()
    get_minecraft_dir()

    mods_dir = minecraft_dir + '/mods'
    mods_dir_exist = os.path.isdir(mods_dir)
    mod_zip = mods_dir + '/mods.zip'

    if not mods_dir_exist:
        print('Creando una ubicacion para alojar los mods...')
        print('')
        os.mkdir(mods_dir)

    for filename in os.listdir(mods_dir):
        file_path = os.path.join(mods_dir, filename)
        try:
            if os.path.isfile(file_path) or os.path.islink(file_path):
                os.unlink(file_path)
            elif os.path.isdir(file_path):
                shutil.rmtree(file_path)
        except Exception as e:
            print('¡Vaya! %s. Reason: %s' % (file_path, e))
            exit(1)

    time.sleep(2)

    url = 'https://asuna.tools/data/downloads/asunamc/mods.zip'

    def show_progress(block_num, block_size, total_size):
        global pbar
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

    urllib.request.urlretrieve(url, mod_zip, show_progress)
    a = 1
    print('')
    print('## Extrayendo mods.zip...')
    print('')
    with ZipFile(mod_zip, 'r') as zipObj:
        zipObj.extractall(mods_dir)
    print('100% |########################################################################|')
    print('')
    print('Eliminando archivos innecesarios...')
    print('')
    os.remove(mod_zip)
    print('100% |########################################################################|')
    time.sleep(1)
    clear()
    menu()


def optifine():
    clear()
    get_minecraft_dir()

    versions_dir = minecraft_dir + '/versions'
    optifine_zip = versions_dir + '/optifine.zip'

    time.sleep(2)

    url = 'https://asuna.tools/data/downloads/asunamc/optifine.zip'

    def show_progress(block_num, block_size, total_size):
        global pbar
        pbar = progressbar.ProgressBar(maxval=total_size)
        pbar.start()

        loaded = block_num * block_size
        if loaded < total_size:
            pbar.update(loaded)
        else:
            pbar.finish()
            pbar = None

    print('## Descargando Optifine...')
    print('')

    urllib.request.urlretrieve(url, optifine_zip, show_progress)
    a = 1
    print('')
    print('## Extrayendo Optifine...')
    print('')
    with ZipFile(optifine_zip, 'r') as zipObj:
        zipObj.extractall(versions_dir)
    print('100% |########################################################################|')
    print('')
    print('Eliminando archivos innecesarios...')
    print('')
    os.remove(optifine_zip)
    print('100% |########################################################################|')
    time.sleep(1)
    clear()
    menu()


menu()
