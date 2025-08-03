import os
import time
import json
import requests
from bs4 import BeautifulSoup
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException, NoSuchElementException, ElementNotInteractableException
import urllib.parse

OUTPUT_DIR = "./DbdPerksApi/wwwroot/perk_icons/"
PERK_LIST_URL = "https://nightlight.gg/perks/list"
JSON_FILE = "./DbdPerksApi/Data/perks.json"
os.makedirs(OUTPUT_DIR, exist_ok=True)

HEADERS = {
    "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64)"
}

def sanitize_perk_name(name):
    # Handle special characters that Nightlight.gg uses in URLs
    # Replace spaces with hyphens
    sanitized = name.strip()  # Trim whitespace
    sanitized = sanitized.replace(" ", "-")
    # Remove apostrophes completely (don't replace with anything)
    sanitized = sanitized.replace("'", "")
    sanitized = sanitized.replace("’", "")
    sanitized = sanitized.replace("‘", "")
    sanitized = sanitized.replace("%E2%80%98", "")
    # Handle ampersands
    sanitized = sanitized.replace("&", "")
    # Handle other special characters
    sanitized = sanitized.replace(":", "")
    sanitized = sanitized.replace("!", "")
    sanitized = sanitized.replace("?", "")
    sanitized = sanitized.replace(",", "")
    sanitized = sanitized.replace(".", "")
    sanitized = sanitized.replace("/", "-")
    # Handle special Unicode characters
    sanitized = sanitized.replace("\u00e2", "a")
    sanitized = sanitized.replace("à", "a")
    sanitized = sanitized.replace("\u2019", "")
    # Handle non-breaking spaces
    sanitized = sanitized.replace("\u00a0", "-")
    sanitized = sanitized.replace("--", "-")
    # Convert to lowercase
    sanitized = sanitized.lower()
    return sanitized

def get_perk_data_with_selenium():
    print("Generating perk data from existing perks.json file...")
    
    perks = {}
    
    # Read the existing perks.json file to get perk names
    try:
        if os.path.exists(JSON_FILE):
            with open(JSON_FILE, "r", encoding="utf-8") as f:
                perks_data = json.load(f)
        else:
            print(f"Warning: {JSON_FILE} not found.")
            return perks
    except json.JSONDecodeError as e:
        print(f"Error reading {JSON_FILE}: {e}")
        return perks

    # Generate perk data based on naming convention
    for json_key, perk_data in perks_data.items():
        if "name" in perk_data:
            perk_name = perk_data["name"]
            # Generate the image URL based on the naming convention
            sanitized_name = sanitize_perk_name(perk_name)
            image_url = f"https://cdn.nightlight.gg/img/perks/{sanitized_name}.png"
            perks[perk_name] = image_url

    print(f"Generated data for {len(perks)} perks.")
    return perks

def download_icon(name, icon_url):
    sanitized_name = sanitize_perk_name(name)
    filename = os.path.join(OUTPUT_DIR, f"{sanitized_name}.png")
    # Check if icon already exists
    if os.path.exists(filename):
        print(f"Skipping {name} (already exists)")
        return True

    try:
        response = requests.get(icon_url, headers=HEADERS)
        response.raise_for_status()
        # Save to data directory
        with open(filename, "wb") as f:
            f.write(response.content)
        print(f"Downloaded {name}")
        return True
    except Exception as e:
        print(f"Failed to download {name}: {e}")
        return False

def update_perks_json(perk_map):
    print("Updating perks.json with new image URLs...")
    try:
        if os.path.exists(JSON_FILE):
            with open(JSON_FILE, "r", encoding="utf-8") as f:
                perks_data = json.load(f)
        else:
            print(f"Warning: {JSON_FILE} not found.")
            return False
    except json.JSONDecodeError as e:
        print(f"Error reading {JSON_FILE}: {e}")
        return False

    # Update image URLs for existing perks
    updated_count = 0
    for json_key, perk_data in perks_data.items():
        if "name" in perk_data:
            perk_name = perk_data["name"]
            # Generate the hosted image URL
            sanitized_name = sanitize_perk_name(perk_name)
            image_url = f"/perk_icons/{sanitized_name}.png"
            
            # Update the image field
            perks_data[json_key]["image"] = image_url
            updated_count += 1
            print(f"Updated {perk_name} image URL to {image_url}")

    try:
        with open(JSON_FILE, "w", encoding="utf-8") as f:
            json.dump(perks_data, f, indent=2, ensure_ascii=False)
        print(f"Successfully updated {JSON_FILE} with {updated_count} image URLs")
        return True
    except Exception as e:
        print(f"Error writing to {JSON_FILE}: {e}")
        return False

def main():
    print("Nightlight.gg Perk Icon Downloader")
    print("========================================")

    perk_map = get_perk_data_with_selenium()
    if not perk_map:
        print("No perks found. Exiting.")
        return

    print(f"\nStarting download of {len(perk_map)} perk icons...")
    print("----------------------------------------")

    success, failed = 0, 0
    for i, (name, icon_url) in enumerate(perk_map.items(), 1):
        print(f"[{i}/{len(perk_map)}] Downloading {name}...")
        if download_icon(name, icon_url):
            success += 1
        else:
            failed += 1
        time.sleep(0.2)

    print("\nUpdating perks.json...")
    if update_perks_json(perk_map):
        print("JSON update completed.")
    else:
        print("JSON update failed.")

    print("\n========================================")
    print("Download Summary:")
    print(f"Successfully downloaded: {success}")
    print(f"Failed downloads: {failed}")
    print(f"Total attempts: {len(perk_map)}")
    print(f"Icons saved to: {OUTPUT_DIR}")

if __name__ == "__main__":
    main()